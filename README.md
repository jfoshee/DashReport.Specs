# DashReport

CLI to generate HTML reports from SQL queries.

## Natural Language Specification

### Feature: Generate HTML Report (no template)

#### Scenario: Simple SQLite Table

Given that there is a SQLite file called `example.db`  
That contains this table named `tblExample`:

| id | name  | age |
|----|-------|-----|
| 1  | Alice | 25  |
| 2  | Bob   | 30  |
| 3  | Carol | 35  |

And there is a query file called `example.sql`  
That contains the query:
```sql
SELECT * FROM tblExample
```
When the CLI is run with the arguments:  
`--connection "Data Source=example.sql" --query-file example.sql`

Then a file is created named `output.html`  
That contains:
```html
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
```

#### Scenario: Simple SqlServer Table

Same as above, except connecting to a local SqlServer database named `DashReportTest`.  
Use connection string `"Server=localhost;Database=DashReportTest;Trusted_Connection=True;TrustServerCertificate=True"`