# JobAdder
Pogramming Assignment for JobAdder - Recruitment app matching best candidates for jobs

**JobAdder Coding Challenge**

For this exercise, we&#39;d like you to create a web application that will help a recruiter automatically match candidates to open jobs.

Within your UI, (the form the interface takes is open to your interpretation), for each job, display a candidate that is the most-qualified to fill that job.

There are many approaches with how this task could be achieved but it will be up to you to analyse and determine what you think is a good way to match a candidate to a job and present it to the UI.

API documentation can be found in the following location: [https://jobadder1.docs.apiary.io/#](https://jobadder1.docs.apiary.io/)

**Business Logic**

Both the Jobs and the Candidates sources contain a list of skills in descending order of job relevance or candidate strength.

To determine the best matches for each job I have summed the multiplication of Job skill relevance weighting withe the Candidate&#39;s skill strength weighting for each matching skill. The list is presented in descending order of summed matching weightings, then be number of matching skills.

The weightings applied to each Job or Candidate skill in descending order of importance are stored in the appsettings.Development.json file. The keys are:

SkillsWeightings:CandidateStrengths

SkillsWeightings:JobRelevance

Their values have been set in descending order from 10 to 1. They can be amended to provide proportionally more importance to the initial skills such as 10, 8, 5, 3, 2,1.

**Data Source API**

It was noted that some candidate skills were duplicated. I assumed that this was an error and any duplicatesd skills for both candidates and jobs are removed prior to having the relevant weightings applied to them.

**Solution Architecture**

The server side is .NET Core 3.1 and the client is Angular 9.1;

The server side BlackAdder solution contains three projects with each project containing a refernce to the subsequent project in the list.

- API (Web API - Controllers, application startup)

- Infrastructure (ClassLib - services implementing interfaces, access external API)

- Core (ClassLib - entities, interfaces, business logic helper classes)

The client side Angular application source code exists in the **client** folder.

It contains lazily loadable feature modules for jobs and candidates.

The core module contains singleton features such as the nav-bar, interceptors and relevant services.

The shared module contains the models. It would also contain components, pipes and other features shared by feature modules.

The URLS for the application are unsecured http for simplicity. **app.UserHttpsRedirection()** has been commented out in the **Startup.Configure()** method.

[http://localhost:5000](http://localhost:5000/) API

[http://localhost:4200](http://localhost:4200/) Angular application and allowed origin for the CORS policy

[http://localhost:5000/swagger/index.html](http://localhost:5000/swagger/index.html) Swagger API documentation

There are several endpoints documented in the Swagger page exposed in the JobsController and CandidatesController that are not currently consumed by the Angular application but could provide additional functionality.

Postman was used to test the APIs. The additional exposed API endpoints permitted testing the IJobService and ICandidateService methods that are reused by other service methods in obtaining the application goal of returning the correct response data for the **JobsController.GetBestMatchedCandidatesForJob(int jobId, int topNumber)** method.

**How to run the application**

The application was developed entirely in Visual Studio Code. Visual Studio 2019 was used only to be able to occasionally run the MSTest unit tests using the Resharper unit test runner which shows the tests hierarchically.

The setup instructions are for opening the folder containing the solution in VS Code and running the following commands from the inbuilt CLI.

**\&gt;dotnet test** (to run all the MSTest unit tests)

To run the server

**\&gt;cd API**

**API\&gt;dotnet run**

To run the Angular client, in another Terminal window

**\&gt;cd client**

**client\&gt;ng serve -o**

This should open the application home page in a browser tab when compiled.

The user can select the Candidates or Jobs navigation menu items for the full list of candidates or open jobs. There is no pagination of the results returned from the server.

The Jobs list permits the user to select the top 5, 10, 20 or 50 matching candidates for any of the listed jobs.

The top 5/10/20/50 matching candidates are displayed in descending matching weighted skills order. The matching skills and the individual weighting calculations are also displayed for each candidate.
