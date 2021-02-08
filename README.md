#URC++ Final Readme
Team Members: Connor Patten, Dan Ruley, Hayden Dalley
URL: https://ec2-3-237-37-178.compute-1.amazonaws.com



Abstract:
	The URC++ team created a more improved version of the URC project. The website is now much more functional, and contains more realistic data because we implemented web scraping to seed the opportunities with real professors from the University of Utah.  This, along with other changes to the UI, greatly improved the look and feel of the project.					
We improved the overall security of the website, scanning all uploaded files for viruses and requiring admin authorization for users that register as a professor. One more area we improved is the Opportunity, users are now able to sort and filter the opportunities. Another improvement is users are now able to register with a specific role, pending administrator approval. In addition, users are able to receive notifications about the latest opportunities added, so they can be aware of new opportunities, and student users are able to directly apply to the opportunity and be able to see the current status of their application. Lastly, toasters are employed in various places throughout the app to better indicate successful/errors on user actions.







Introduction
The URC++ team created a more improved version of the URC project. The website is now much more functional, and contains more realistic data because we implemented web scraping to seed the opportunities with real professors from the University of Utah.  The overall look and feel of the website has improved significantly with the features our team implemented, and the URC feels like a much more functional project overall.
We improved the overall security of the website, scanning all uploaded files for viruses and requiring admin authorization for users that register as a professor. Another improvement is users are now able to register with a specific role, pending administrator approval. Uploaded files are restricted to certain sizes and input formats, in addition to the virus scanning.  
Another feature we added is the notification for students. Every time a new opportunity is created, the students will receive a notification about the opportunity to alert them of it. Every student will receive the notification when an opportunity is created. The student is able to mark the notifications as read individually are all at the same time.
	We also added the application status feature to the project. Students are now able to apply to opportunities directly from the opportunity list page. A new page called the application status is where the students can view their application status for each opportunity that they applied to. The professors are also able to see the application for each of their opportunities in a new page. On this page the professor can reject or mark the application as under review. The student will see this change reflected in the application status page.
A smaller feature we added is using toasters in various places throughout the application. These toasters are used to indicate to the user whether the action he/she did was a success or an error. For example, when applying to an opportunity, the user will see the toaster message in the top right with a message similar to “Successfully applied to the Research Opportunity”. This can better inform the user that his/her action actually resulted in something happening, especially when the action keeps them on the same page.
One more area we improved is all of the table views for Opportunities and the three admin tables for Students, Opportunities, and Users/Roles.  These tables now are paginated so the views look nice with all of the seeded data.  Additionally, these tables offer features so the user can search through the table by certain keywords, or sort the table in ascending/descending order by a given field.


Feature Table
Feature Name
Scope
Primary Programmer
Time Spent
File
/Function
LoC
Notifications
Full
Hayden
9hrs
Notifications Model

added controller methods

html/css
~350
Toasters
Front
Hayden
2hrs
Add various toasters to  indicate success/errors of various actions
~120
Application Status
Mostly Front, but some backend
Hayden
9 hrs
Added apply buttons to opportunities for student,

Added student application status view

Added Professor Opportunity application view
~530





Feature Table (cont.)
Feature Name
Scope
Primary Programmer
Time Spent
File
/Function
LoC
Improved
Opportunities Table
Full
Dan
6hrs
Added table pagination for the main opportunities view.  

Added search/sort features for opportunities.

Improved overall look and feel of the view.
~400
Improved Admin Views
Full
Dan
5hrs
Added table pagination for the admin users/roles, opportunities, and student views.

Added search/sort features for the admin table views.

Improved overall look and feel of the view.
~500
Web Scraping/
DB Seeding
Backend
Dan
8 hrs
Used web scraping to seed opp/users DB with UofU CS profs.

Seeded Users DB with students, scraping random names, interests, skills.

Scraped U prof. pictures and then random tech pictures for generic images
~600
Role Assignment in Registration
Full
Connor
2 Hrs
Allows User to specify which role they would like to register as. Once they have completed the email confirmation they will be able to sign in and access the necessary pages. 
~50
Calendar Rss feed and other features to a footer for the home page
Full
Connor
8 hrs
Application now pulls event data from the Computer Science Department Calendar and displays a few of the events dynamically. 
~100
Improved security on File uploads
Backend
Connor
10 Hrs
Application now has a greater number of validation checks for uploaded files including specified file types and length. Also verifies the request using an authorization token in the cookies. Before saving any files to the file system a third party virus scanner is run to verify no malicious content is being uploaded.
~100


Individual Contribution Table


Team Member
Time Spent on Proj
Lines of Code Committed
Connor
20
~300
Dan
21 hrs
~1500
Hayden
20 hrs
~1000


Hayden
I added toasters to various places around the application to indicate that various actions were successful or to indicate an error had occurred. I also added notifications feature to the application for the student users of the app. Lastly, I added an application status feature that allows students to apply to opportunities and monitor the status of their application in regards to the opportunity.

Dan
I implemented web scraping to seed the opportunities and users database with more interesting and useful data.  The opportunities all correspond to real U CS professors and their research interests and tags are actual data scraped from the U website or google scholar.  I also overhauled all of the table views and implemented table pagination along with searching/sorting features so that the opportunities and all of the admin/professor tables are much more nice looking and useful.  I also did a couple of hours of troubleshooting with getting selenium running on the AWS instance and dealing with some of the interesting issues that popped up while trying to implement web scraping remotely.



Connor
I added a calendar system to the home page. These events are pulled from the Computer Science Department’s calendar. Several of these events are displayed in a list at the bottom of the page. This involved linking the project to the RSS feed for the University’s calendar and reading the feed. I also set up a way for users to pick a role when registering, this allows users to start using the site as soon as their email is verified and not have to wait for an administrator to assign them a role. I also increased the security of the file upload system. More validation checks were added including file size and file type. The process also now uses authorization tokens as well as a third party virus scan on file inputs before being saved to the file system to reduce chances of malicious activity. 
Performance Level
Overall, we feel the group achieved a good performance level.  While there were some features we wish we had the time to implement, the final result definitely achieves significant improvements compared to the original URC project.  The functionality of the features we added, combined with the improvements to the overall UI make the website feel much more similar to a modern, professional website.
Summary
URC++ has added improved Opportunity list that allows the user to filter and search through the opportunities as compared to before. Also the Opportunities are based on real world data instead of made up data, which makes the application look more polished and indicative of real world use. We also added toasters to improve user experience to inform users of successful/erroneous actions. Another feature we added is notifications that will inform students when new opportunities become available, so that students don’t have to search for the latest opportunity themselves. Additionally we added the application status feature to allow students to apply for opportunities. This feature is helpful because it helps the professor know what students are interested in their research opportunity and what skills they have. It also benefits the student because they can see their application’s status and when it was updated.






# PS6 URC Readme

Author: Dan Ruley
Partner: None
Date: October, 2020
Course: CS 4540, University of Utah, School of Computing 
Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

Deployed URL: https://ec2-52-87-158-131.compute-1.amazonaws.com/
Github Page: https://github.com/uofu-cs4540-fall2020/urc-ps1-dan-ruley
Application activation api documentation: https://documenter.getpostman.com/view/13221590/TVYF8dya
Student summary get api documentation: https://documenter.getpostman.com/view/13221590/TVYF8e3q

Comments to Evaluators:

-Thanks for all of the help!

Assignment Specific Write-up:

Backend validation
------------------
-Student summary must be greater than 20 characters and less than 1500 characters
-GPA must be in a valid range: 0.0 to 4.0
-uID is checked to ensure it is a 'u' followed by 7 numbers.
-Hours per week must be between 5 and 60
-Application submit time and edit times are handled by the backend to ensure that the creation date is
only updated when the app is submitted, and that the modify date is changed on edit and creation.


Frontend validation
-------------------
-Uploading files: image files and resume files can be uploaded to the server.  However, I made sure to check that:
-Exactly one file has been selected
-File size must be >0
-File size cannot be greater than 10MB.

Getting the student summary:
-The front end checks that the search textbox has at least some text in it, the request will not proceed if it is empty.
-If the frontend detects that no users were found by the search, it displays a simple message instead of a weird looking blank table.

Peers Helped:

-na

Peers Consulted:

-na

Acknowledgements:

The background image was taken from: https://www.linkedin.com/school/university-of-utah/


References:

-Image/resume file uploads are based largely on the class example shown in lecture 8.






# PS5 URC Readme

Author: Dan Ruley
Partner: None
Date: October, 2020
Course: CS 4540, University of Utah, School of Computing 
Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

Deployed URL: https://ec2-54-208-75-248.compute-1.amazonaws.com/
Github Page: https://github.com/uofu-cs4540-fall2020/urc-ps1-dan-ruley

Comments to Evaluators:

-Thanks for all of the help!  It was a long road but I learned a ton along the way.

Assignment Specific Write-up:

-Authentication/Authorization:  I started to learn how much of an intricate process this is.  Even using the Identity scaffolding, there are many details that you need to get right so that everything works correctly.  I do appreciate the power of Entity/Identity when building the website.  Using the User/Role Manager abstractions, it was very easy to inject the needed dependencies into the views so that the login status and role of a user affects what is displayed.  Also, I really appreciated the simple Role Authorization tags you put in the Controllers to restrict page views to certain roles.  There was a bit of a learning curve with figuring out how to use the AJAX POST method and update the roles DB correctly, but I appreciate how useful jquery is to offer this kind of dynamic functionality.

-Improvements:  I cleaned up the navbar and made some of the boilerplate VS generated code look a bit nicer.  Role specific links are included on the navbar as well as the dropdown menus.

Peers Helped:

-na

Peers Consulted:

-na

Acknowledgements:

The background image was taken from: https://www.linkedin.com/school/university-of-utah/


References:

I used this article http://www.binaryintellect.net/articles/5e180dfa-4438-45d8-ac78-c7cc11735791.aspx as a reference for some of the DB seeding process.













# PS4 URC Readme

Author: Dan Ruley Partner: None Date: September, 2020 Course: CS 4540, University of Utah, School of Computing Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

Deployed URL: https://ec2-54-226-252-200.compute-1.amazonaws.com/Home/Index
Github Page: https://github.com/uofu-cs4540-fall2020/urc-ps1-dan-ruley

Comments to Evaluators:

-na

Assignment Specific Write-up:

-Skills/Tags:  Unfortunately, I did not have time to create the seperate tables for the Skills and Tags.  Instead, I just included them as a String.

-Images:  Also, I did not have time to complete the image uploading.  I use hardcoded images stored in the image files for the opportunity images.

Peers Helped:

-na

Peers Consulted:

-na

Acknowledgements:

The background image was taken from: https://www.linkedin.com/school/university-of-utah/


References:

W3 Schools CSS Tutorial: https://www.w3schools.com/css/
W3 Schools HTML Tutorial: https://www.w3schools.com/html/default.asp
Lecture videos/slides




# PS2 URC Readme


Author:    Dan Ruley
Partner:   None
Date:      September, 2020
Course:    CS 4540, University of Utah, School of Computing
Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

Deployed URL:  localhost
Github Page:   https://github.com/uofu-cs4540-fall2020/urc-ps1-dan-ruley

Comments to Evaluators:

  -na



Assignment Specific Write-up:


Bootstrap elements I chose to include:

-Navbar with dropdown menu containing overall site navigation links

-Jumbotron on the Home Page that displays a greeting to the user.

-Carousel on the Home Page that displays testimonials

-Form elements on the Student Application page that makes up the Application


How bootstrap impacted my site:

  I think the use of bootstrap improved my website overall.  I added a Jumbotron and a carousel to the home page, which definitely make the home page a lot more interesting than it was before.  The carousel displays some example testimonials from students and professors who used the URC.  Bootstrap also aided my site immensly when I added the form to the student application page.  Without bootstrap, I imagine that creating these forms by hand using HTML/CSS would be a bit of a headache.  Bootstrap made it easy to make a serviceable and clean-looking application form.  
  

Impact of my UI on site usability:

  I think that several of my UI choices improve my site's overall usability.  One of the main ones is the open layout, including mostly large elements on each page so that things feel easy to navigate and not too cluttered.  Ease of navigation is one thing I strove for with the UI; the website layout has three different locations at which the user can link to the other pages on the website.  These are the dropdown menu at the top-left, the links on the Navbar at the top-right, and the links at the footer at the bottom-left.  That way, if the user wants to switch to a different page on the website, chances are good that their cursor is already resting only a short distance from a handy link somewhere on the page.


Peers Helped:

  -na


Peers Consulted:

  -na


Acknowledgements:

   Bootstrap elements were modified from examples provided by the Get Bootstrap website.
   
   Jumbotron - https://getbootstrap.com/docs/4.5/components/jumbotron/
   
   Carousel - https://getbootstrap.com/docs/4.5/components/carousel/
   
   Forms for the application - https://getbootstrap.com/docs/4.5/components/forms/
   
   Navbar - https://getbootstrap.com/docs/4.5/components/navbar/ and https://getbootstrap.com/docs/4.5/components/dropdowns/



References:

   -na



# PS1 URC Readme

Author:    Dan Ruley
Partner:   None
Date:      September, 2020
Course:    CS 4540, University of Utah, School of Computing
Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

Deployed URL:  localhost
Github Page:   https://github.com/uofu-cs4540-fall2020/urc-ps1-dan-ruley

Comments to Evaluators:

  -na

Assignment Specific Write-up:

  Overall, I tried to keep the site design open and easy to read.  The font sizes are fairly large and the background image and css styles gives the website a "light" feel to it.  One specific UI/UX decision I made was to add a slight light-gray shadow to the Header text on eack page.  This, combined with the black text color, gives a nice contrast to the colorscheme of the background image.  I tried to make the presented data easily understandable by organizing it into CSS-styled HTML tables.

Peers Helped:

  -na

Peers Consulted:

  -na

Acknowledgements:
   
   The background image was taken from: https://www.linkedin.com/school/university-of-utah/
   
   The button linkes I used were inspired by https://www.w3docs.com/snippets/html/how-to-create-an-html-button-that-acts-like-a-link.html; however,
   I did modify this significantly.
  

References:

   1.  W3 Schools CSS Tutorial: https://www.w3schools.com/css/
   2.  W3 Schools HTML Tutorial: https://www.w3schools.com/html/default.asp
   3.  Lecture videos/slides
