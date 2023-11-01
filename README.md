# ChatOn

## ChatOn - Api

In the Package Terminal on ChatOn.Models :

- Make the initial migration

```
  Add-Migration [MigrationName]
```

- Update your database

```
  Update-Database
```

- Execute the following script on your database

  [script/insert-script.sql](scripts/database/insert-script.sql)

- Then run the .NET project

## ChatOn - WebApp

In a terminal at /ChatOn.WebApp :

- Install the dependencies

```
  npm install
```

- Then run the development server

```
  npm run dev
```

Open [http://localhost:3000](http://localhost:3000) with a browser to view the Web Application.
