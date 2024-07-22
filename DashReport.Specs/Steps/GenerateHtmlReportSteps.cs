using System.Data.SQLite;
using DashReport.Specs.Drivers;

namespace DashReport.Specs.Steps;

[Binding]
public sealed class GenerateHtmlReportSteps
{
    private string? _databaseFilePath;
    private string? _queryFilePath;
    private string? _outputFilePath;
    private string? _cliArguments;

    private readonly CliDriver _cliDriver;

    public GenerateHtmlReportSteps(CliDriver cliDriver)
    {
        _cliDriver = cliDriver;
    }

    [Given(@"there is a SQLite file called ""(.*)""")]
    public void GivenThereIsASQLiteFileCalled(string databaseFileName)
    {
        _databaseFilePath = Path.Combine(Directory.GetCurrentDirectory(), databaseFileName);
        if (File.Exists(_databaseFilePath))
        {
            File.Delete(_databaseFilePath);
        }

        SQLiteConnection.CreateFile(_databaseFilePath);
    }

    [Given(@"it contains a table named `tblExample` with the following data:")]
    public void GivenItContainsATableNamedTblExampleWithTheFollowingData(Table table)
    {
        if (_databaseFilePath is null)
        {
            throw new InvalidOperationException("Database file path is not set.");
        }

        using var connection = new SQLiteConnection($"Data Source={_databaseFilePath};Version=3;");
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"
            CREATE TABLE tblExample (
                id INTEGER PRIMARY KEY,
                name TEXT,
                age INTEGER
            );";
        command.ExecuteNonQuery();

        foreach (var row in table.Rows)
        {
            command.CommandText = $"INSERT INTO tblExample (id, name, age) VALUES ({row["id"]}, '{row["name"]}', {row["age"]});";
            command.ExecuteNonQuery();
        }

        connection.Close();
    }

    [Given(@"there is a query file called ""(.*)"" containing the query:")]
    public void GivenThereIsAQueryFileCalledContainingTheQuery(string queryFileName, string query)
    {
        _queryFilePath = Path.Combine(Directory.GetCurrentDirectory(), queryFileName);
        File.WriteAllText(_queryFilePath, query);
    }

    [Given(@"a file named ""(.*)"" does not exist")]
    public void GivenAFileNamedDoesNotExist(string fileName)
    {
        _outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        if (File.Exists(_outputFilePath))
        {
            File.Delete(_outputFilePath);
        }
    }

    [When(@"the CLI is run with the arguments: `(.*)`")]
    public void WhenTheCLIIsRunWithTheArguments(string cliArguments)
    {
        _cliArguments = cliArguments;
        var arguments = _cliArguments.Split(' ');

        _cliDriver.Run(arguments);
    }

    [Then(@"a file named ""(.*)"" is created")]
    public void ThenAFileNamedIsCreated(string outputFileName)
    {
        _outputFilePath = Path.Combine(Directory.GetCurrentDirectory(), outputFileName);
        File.Exists(_outputFilePath).Should().BeTrue();
    }

    [Then(@"the file contains:")]
    public void ThenTheFileContains(string expectedContent)
    {
        if (_outputFilePath is null)
        {
            throw new InvalidOperationException("Output file path is not set.");
        }

        var actualContent = File.ReadAllText(_outputFilePath).Trim();
        actualContent.Should().Be(expectedContent.Trim());
    }
}
