# Movie Night Planner

![List](Resources/cover1.png)

## What is it?
While essentially a tech demo of MVC ASP.NET Core and EF Core, this site allows users to plan and vote for movies.

## What's currently possible?
* Working users/sessions
    * Authentication
    * Both regular and administrative accounts
    * Session-based browsing
* User features
    * Posting movies/shows using only the url (data queried from external API)
    * Browsing through submissions
    * Voting on submissions
* Administrative features
    * Managing accounts
        * Creating accounts
        * Resetting passwords
        * Deleting accounts
    * Managing submissions
        * Deleting submissions
        * Marking submissions as watched
        * Resetting votes

## Note about account registration
Since this is a site focused on smaller communities, registration is intended to work via referral. 
An administrative account has to be used to create new users.
Password will be set on the user's first login.
