# Create a container with the compiled asp.net core app.
# Created by Nikhil Kanotra
FROM microsoft/dotnet:2.2-aspnetcore-runtime

# Create app directory
WORKDIR /app
COPY src/SiteConfiguration.API/out .
#COPY /src/Template/out .

ENTRYPOINT ["dotnet", "SiteConfiguration.API.dll"]





