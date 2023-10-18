# BowlingApp
Bowling Api Project
Standard Pins : 10
TwelvePin Game :12 

Implemented Factory design patteren based on user input "useTwelvePins". If this is true then it will instantiate the TwelvePinclass in service layer , if that value is provided as false it will instantiate the standard 10 Pin class at runtime .

Run The project api , execute the Play Post method with sample valid json response as 

Example : rolls should be 21 


{
  "rolls": [7, 1, 9, 1, 7, 1, 7, 2, 7, 2, 6, 3, 10, 0, 8, 1,10, 0, 10, 10, 9],
  "retrieveScore": true,
  "useTwelvePins": false
}


![image](https://github.com/manipathak07/BowlingApp/assets/45695808/f56db1e8-6f5f-4a76-945c-a037a257cf82)
