using Newtonsoft.Json;
using Questao2;
using System;

public class Program
{
    private static readonly HttpClient client = new HttpClient();
    public static async Task Main()
    {
        string teamName = "Paris Saint-Germain";
        int year = 2013;
        int totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team "+ teamName +" scored "+ totalGoals.ToString() + " goals in "+ year);

        teamName = "Chelsea";
        year = 2014;
        totalGoals = await GetTotalScoredGoals(teamName, year);

        Console.WriteLine("Team " + teamName + " scored " + totalGoals.ToString() + " goals in " + year);

        // Output expected:
        // Team Paris Saint - Germain scored 109 goals in 2013
        // Team Chelsea scored 92 goals in 2014
    }

    public static async Task<int> GetTotalScoredGoals(string team, int year)
    {
        string url = $"https://jsonmock.hackerrank.com/api/football_matches?year={year}";

        var response = await client.GetStringAsync($"{url}&team1={team}&page=1");
        var listTeam1 = JsonConvert.DeserializeObject<ObterListaJogosDto>(response);

        response = await client.GetStringAsync($"{url}&team2={team}&page=1");
        var listTeam2 = JsonConvert.DeserializeObject<ObterListaJogosDto>(response);


        await GetTotalByPages(listTeam1, url, $"&team1={team}");
        await GetTotalByPages(listTeam2, url, $"&team2={team}");

        return listTeam1.Data.Sum(x => int.Parse(x.Team1goals)) + listTeam2.Data.Sum(x => int.Parse(x.Team2goals)); 
    }

    public static async Task GetTotalByPages(ObterListaJogosDto dto, string url, string filter)
    {
        for(int i = 2; i <= dto.Total_Pages;i++)
        {
            var response = await client.GetStringAsync($"{url}{filter}&page={i}");
            var list = JsonConvert.DeserializeObject<ObterListaJogosDto>(response);

            if (list != null && list.Data.Any())
                dto.Data.AddRange(list.Data);
        }
    }

}