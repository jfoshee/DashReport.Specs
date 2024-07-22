Feature: Generate HTML Report (no template)

  Scenario: Simple SQLite Table
    Given there is a SQLite file called `example.db`
      And the SQLite file contains a table named `tblExample` with the following data:
        | id | name  | age |
        | 1  | Alice | 25  |
        | 2  | Bob   | 30  |
        | 3  | Carol | 35  |
      And there is a query file called `example.sql` containing the query:
        """
        SELECT * FROM tblExample
        """
      And a file named `output.html` does not exist
    When the CLI is run with the arguments: `--connection "Data Source=example.db" --query-file example.sql`
    Then a file named `output.html` is created
      And the file contains:
        """
        <table>
          <thead>
            <tr>
              <th>id</th>
              <th>name</th>
              <th>age</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>1</td>
              <td>Alice</td>
              <td>25</td>
            </tr>
            <tr>
              <td>2</td>
              <td>Bob</td>
              <td>30</td>
            </tr>
            <tr>
              <td>3</td>
              <td>Carol</td>
              <td>35</td>
            </tr>
          </tbody>
        </table>
        """

  Scenario: Simple SqlServer Table
    Given there is a SqlServer database named `DashReportTest`
      And the SqlServer database contains a table named `tblExample` with the following data:
        | id | name  | age |
        | 1  | Alice | 25  |
        | 2  | Bob   | 30  |
        | 3  | Carol | 35  |
      And there is a query file called `example.sql` containing the query:
        """
        SELECT * FROM tblExample
        """
      And a file named `output.html` does not exist
    When the CLI is run with the arguments: `--connection "Server=localhost;Database=DashReportTest;Trusted_Connection=True;TrustServerCertificate=True" --query-file example.sql`
    Then a file named `output.html` is created
      And the file contains:
        """
        <table>
          <thead>
            <tr>
              <th>id</th>
              <th>name</th>
              <th>age</th>
            </tr>
          </thead>
          <tbody>
            <tr>
              <td>1</td>
              <td>Alice</td>
              <td>25</td>
            </tr>
            <tr>
              <td>2</td>
              <td>Bob</td>
              <td>30</td>
            </tr>
            <tr>
              <td>3</td>
              <td>Carol</td>
              <td>35</td>
            </tr>
          </tbody>
        </table>
        """
