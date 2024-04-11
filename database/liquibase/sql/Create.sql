CREATE TABLE Users (
  userId text PRIMARY KEY,
  username text
);

CREATE TABLE Commits
( 
   commitId serial PRIMARY KEY,
   userId text REFERENCES Users (userId),
   createdDate timestamp,
   message text,
   diff text,
   summary text
);
