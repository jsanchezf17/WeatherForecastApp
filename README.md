# WeatherForecastApp

To run the weather forecast app go over the next instructions

# Create Database Via Restore Method

To restore database, you will need to create a MSSQL SQLExpress Instance and restore database provided

    -Using SQL Server Management Studio (SSMS):

    -Install SQL Server Express:

    -If you haven't already installed SQL Server Express, download and install it from the official Microsoft website.
    Open SQL Server Management Studio (SSMS):

    -Launch SQL Server Management Studio.
    -Connect to SQL Server Express:

    -In the "Connect to Server" window, enter the server name. For SQL Express, it's often in the format: .\SQLEXPRESS or localhost\SQLEXPRESS.

    -Choose the authentication method (usually Windows Authentication or SQL Server Authentication) and provide the necessary credentials.

After Server has been created you will need to do a restore using the attached .bak file (WeatherForecastDB.bak)

	- Using SQL Server Management Studio (SSMS):

	- Connect to your SQL Server instance.

	- In the Object Explorer, right-click on "Databases" and select "Restore Database."

	- In the "Restore Database" dialog, select "Device" under the "Source" section.

	- Click the "Browse" button to select the backup file (WeatherForecastDB.bak) you want to restore from.

	- The selected backup file will appear in the "Backup media" box. Click "OK."

	- In the "To database" field, enter the name of the database you want to create from the backup. Use the same name as the original database to avoid connection issues with project.

	- Review the restore options, such as the backup sets, and configure them as needed.

	- Click "OK" to start the restore process.


-----------------------
To run project, open WeatherForecast.sln and debug project

Swagger can be used to test API Endpoints