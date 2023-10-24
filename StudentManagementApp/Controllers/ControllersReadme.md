Note:
<br/>
For additional security controller endpoints designed for students retrieve data based on the ids of the students
stored in the database, and the frontend sends the id with the JWT token to the backend. The backend then extracts
the id from the JWT token and uses it to retrieve the data from the database. This is done to prevent students from
accessing data that does not belong to them.