# OpenCrib.Backend

####################################LIST OF ALL CURRENT API ENDPOINTS AND THEIR USE###################################

BASE URL :
https://opencribdevapi.azurewebsites.net/api/

[GET] ALL USERS:
https://opencribdevapi.azurewebsites.net/api/User/GetAllUsers

[GET] FOLLOW USER:
https://opencribdevapi.azurewebsites.net/api/User/FollowUser/{myUserId}/{userId}

GET USER INFO:
https://opencribdevapi.azurewebsites.net/api/User/GetUser/{username}

NEW USER:
https://opencribdevapi.azurewebsites.net/api/User/NewUser

DELETE USER:
https://opencribdevapi.azurewebsites.net/api/User/Delete/{id}

NEW PARTY:
https://opencribdevapi.azurewebsites.net/api/Party/NewParty

RSVP PARTY:
https://opencribdevapi.azurewebsites.net/api/Party/RSVP/{myUserId}/{partyId}

POST A COMMENT:
https://opencribdevapi.azurewebsites.net/api/Party/PostComment

GET PARTIES NEARBY:
https://opencribdevapi.azurewebsites.net/api/Party/GetPartiesNearby/{zipCode}/{range}

####################################LIST OF SUGGESTED ENDPOINTS###################################

# ALLOW A USER INTO YOUR PARTY
