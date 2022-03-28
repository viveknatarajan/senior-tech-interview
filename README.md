# senior-tech-interview

API:
1. Create a new .NET project that will host three endpoints:
   1. GET /patients
   2. GET /patients/{patientId}
   3. POST /login (This should take in an email and password and return the auth information else 401 if the creds are invalid. You may stub/hardcode this.)
2. Endpoints should be protected by authentication. You may stub/hardcode this but callers must provide an auth header of the form: "Bearer {authToken}"
3. The patient endpoints should internally call into a service that we have provided for you at the following url: https://ti-patient-service.azurewebsites.net
   1. Fetch the list of patients by sending a GET request to /patients
   2. Fetch a specific patient by sending a GET request to /patients/{patientId}


Front End:
1. Create a simple react app with at least two views
   1. Login screen that takes in email and password (this should call your /login endpoint)
   2. Patient view (you can redirect here on successful login)
2. Display a list of patients returned
3. When a patient is clicked, show a view of that specific patient (you can do this however you'd like - another route is fine or you can just display another panel underneath your table)
4. Don't worry too much about styling, but do make it look clean.
