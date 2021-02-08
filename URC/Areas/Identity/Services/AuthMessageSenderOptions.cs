﻿/* Author:    Dan Ruley
  Partner:   None
  Date:      October, 2020
  Course: CS 4540, University of Utah, School of Computing
  Copyright: CS 4540 and Dan Ruley - This work may not be copied for use in Academic Coursework.

  I, Dan Ruley, certify that I wrote this code from scratch and did
  not copy it in part or whole from another source.  Any references used
  in the completion of the assignment are cited in my README file and in
  the appropriate method header.

 This class stores the sendgrid API keys it retrieves from the vault file
 */

namespace URC.Areas.Identity.Services
{
    public class AuthMessageSenderOptions { 
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
