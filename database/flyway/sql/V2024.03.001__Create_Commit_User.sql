CREATE TABLE Users (
  userId integer PRIMARY KEY,
  username varchar
);

CREATE TABLE Commit 
( 
   commitId integer PRIMARY KEY,
   userId integer REFERENCES Users (userId),
   createdDate timestamp,
   message text,
   diff text,
   summary text
);
