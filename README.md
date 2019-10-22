# Simple-Web-Crawler
A simple web crawler using Net Core 2.2 and sql server 2017 to store the harvisted data.

# Overview

This is a .Net core console application which simply sends a get request to a targeted url, gel all the html content and store it in a string. Convert that html string to a Html doc using HtmlAgilityPack and extract the required informatio using XPath expressions. Finally, it stores this information in a local database for further investigations.

# Prerequisites:
1. .Net Core 2.2
2. HtmlAgilityPack
3. EntityFramework Core 2.2 (for db communications & scaffolding)

# Recommended Pack
Visual studio 2017 or higher
SSMS 2017 or higher

Note: the db used in this app is availble as a script file, just execute it in an sql server and it shall create the db.
**Do not forget to modify your connection string in the (_WebCrawlerContext) class accordingly


# Goals

This app collects all the open tenders on https://www.libyantenders.ly/ site, with their titles, Sectors and closing dates
what should we extract from this data ?

If you find this code useful, just inform me with any suggeted enhancements or maybe a url where we can extract some useful data
