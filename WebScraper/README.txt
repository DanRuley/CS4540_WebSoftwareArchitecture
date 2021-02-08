PS7 README
-------------------------------------------------------------------------------------------------------------------
Author:    Dan Ruley
Partner:   None
Date:      11/10/2020
Course:    CS 4540, University of Utah, School of Computing
Copyright: CS 4540 and Dan Ruley- This work may not be copied for use in Academic Coursework.

Comments to Evaluators:
-Thanks!

Assignment Specific Write-up:
1.) I reviewed Ping Cheng Chung, uID: u1176174

2.) I chose an implicit wait value of 5 seconds.  The reason I chose this value is that I feel like it is about the longest a typical user would ever want to wait for something on the web before starting to suspect something went wrong.  In actuality, it is probably actually more like 2-3 seconds these days, but I wanted some allowance for slower connections and so forth.  This value did not seem to affect my program very much when it was set to anything above 2.  However, when the implicit wait was not set, or it was set to only one second, sometimes the driver would throw exceptions because it tried to access elements before they loaded.

3.)
In stark contrast to Windows Form Designer (which is a buggy mess in my humble opinion =D), Selenium was actually really easy and fun to use.  Once I got the hang of how you use the driver to navigate through different sections of the html, it was fairly straightforward.  I found that there is something very enjoyable about programming a computer to browse the web.  The only real issue I think is that this program is quite fragile the way it is written.  Due to less than stellar website design, there are some elements that are very hard to access without using XPaths.  I imagine that these change frequently, and it would need constant maintanence to keep working.

I think it would be very easy to apply selenium to test URC functionality.  In fact, this assignment made me realize that some of my URC html could be improved by adding more id tags.  I think if I combed back through all of the URC view pages and made sure every element has a unique and relevant ID, then it would be a cinch to code up a selenium app to scrape and test the functionality.  It is interesting how using Selenium makes me understand more about how create well-structured HTML.  Overall, this was probably my favorite assignment of the class so far!

Peers Helped:
-na

Peers Consulted:
-Ping Cheng Chung (for the peer review)

Acknowledgements:
Selenium

References:
I based my file saving off of code from my 3500 Spreadsheet.