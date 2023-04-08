# CoreProjects
This repo is for DotNet Core project: 

#**How to use the project and run/test it**
1. Download the MyWebApp.zip file on you local system. 
2. I have implemented and tested this project in visual studio 2022 so make sure to use 2022 if earlier versions doesn't support it. 
3. Unzip the downloaded MyWebApp.zip folder. 
4. Nagivate to "MyWebApp.sln" solution.
5. Open this MyWebApp.sln in Visual Studio.
6. Right click on Project and build/rebuild it to compile or you can directly run which will first build and then run.
   (OR) After opening this, just press f5 or start button in Visual studio to start the service on given port in the code. 
7. It must launch cmd/powershell which shows that the service has started on the given port. 
8. Open Postman tool
9. Download POST API collection from this repo: GWP Copy 2.postman_collection.json
10. Import this API collection on Postman. 
11. Send the POST request from Postman and verify the response.
12. Other thing you can also do is to put a break point in program.cs in httpPost method and see how the code is working if you want to debug.

#**Implementation Details:**

I have implemented the gwp controller in program.cs just to simplify the things. 

Program.cs has logic for:
1) Reading the .csv file to a in-memory mockup database called DataModel
2) Populating the values to the in-memory database
3) Handling the HttpPost request at server and then calculates the average for each linesOfBusiness and returning the response for this POST API.
4) Also added some error handling in the code.  




