# Create a container with the compiled asp.net core app.
# Created by Nikhil Kanotra
FROM microsoft/dotnet:2.2-aspnetcore-runtime

# Create app directory
WORKDIR /app
COPY src/DeviceCommunication/out .
#COPY /src/Template/out .

ENTRYPOINT ["dotnet", "DeviceCommunication.API.dll"]





