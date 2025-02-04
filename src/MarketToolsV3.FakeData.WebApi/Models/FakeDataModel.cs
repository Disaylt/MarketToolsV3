﻿

using MarketToolsV3.FakeData.WebApi.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Models
{
    public class FakeDataModel
    {
        public string? Tag { get; set; }
        public TaskCompleteCondition TaskCompleteCondition { get; set; }
        public int NumberOfRepetitions { get; set; }
        public int NumSuccessful { get; set; }
        public int NumFailed { get; set; }
        public int TimeoutBeforeRun { get; set; }
        public string? Path { get; set; }
        public bool SkipIfPreviousTaskNotComplete { get; set; }

        [Required(ErrorMessage = "Необходимо ввести метод HTTP запроса.")]
        public required string Method { get; set; }
        public JsonNode? Body { get; set; }
    }
}
