﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TvMaze.Data;
using TvMaze.Data.Interfaces;
using TvMaze.Interfaces;
using TvMaze.Models;

namespace TvMaze.Services
{
    public class TVMazeService : ITVMazeService
    {
        const string showUrl = "/shows";
        const string castUrl = "/shows/{0}/cast";
        private readonly ITVMazeRepository _tvMazeRepository;
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public TVMazeService(ITVMazeRepository tvMazeRepository, ILogger<TVMazeService> logger, HttpClient httpClient)
        {
            _tvMazeRepository = tvMazeRepository;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task StartScrapingAsync()
        {
            try
            {
                var shows = await ProcessTVShows();

                foreach (var show in shows)
                {
                    var castList = await ProcessCastmembers(show.Id);

                    if (castList.Any())
                    {
                        var exsitingLinkages = await _tvMazeRepository.GetCastShowLinkageAsync();
                        var linkageList = castList.ToList().Select(cm => new CastShowLinkage() { CastmemberId = cm.Id, TVShowId = show.Id })
                            .Where(cm => !exsitingLinkages.Any(link => link.CastmemberId == cm.CastmemberId && link.TVShowId == cm.TVShowId))
                            .GroupBy(cm => new { cm.CastmemberId, cm.TVShowId }).Select(x => x.First());

                        _tvMazeRepository.AddShow(show);
                        _tvMazeRepository.AddCastmembers(castList);
                        _tvMazeRepository.AddCastShowLinkage(linkageList);
                        _tvMazeRepository.Save();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        private async Task<IEnumerable<TVShow>> ProcessTVShows()
        {
            var result = await ScrapeShowsAsync();

            return result;
        }

        private async Task<IEnumerable<Castmember>> ProcessCastmembers(int showId)
        {
            var castList = await ScrapCastAsync(showId);
            var existingCastList = await _tvMazeRepository.GetCastmemberIdListAsync();

            var CastmemberAddList = existingCastList.Any() ?
                (castList.Where(s => !existingCastList.Contains(s.Id)).GroupBy(p => p.Id).Select(x => x.First())) :
                castList;

            return CastmemberAddList;
        }

        public async Task<IEnumerable<TVShow>> ScrapeShowsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync(showUrl);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var existingShowIds = await _tvMazeRepository.GetShowIdListAsync();
                return JsonConvert.DeserializeObject<IEnumerable<TVShow>>(result).Where(s => !existingShowIds.Contains(s.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<TVShow>();
            }
        }

        public async Task<IEnumerable<Castmember>> ScrapCastAsync(int showid)
        {
            try
            {
                var castresponse = await _httpClient.GetAsync(string.Format(castUrl, showid));
                var stringResult = await castresponse.Content.ReadAsStringAsync();
                var castList = JsonConvert.DeserializeObject<IEnumerable<CastmemberModel>>(stringResult);
                return castList.Select(castmem => castmem.person).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new List<Castmember>();
            }
        }
    }
}
