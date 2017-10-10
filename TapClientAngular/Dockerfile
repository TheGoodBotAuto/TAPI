FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY /bin/Debug/netcoreapp2.0/publish /app
ENV ASPNETCORE_URLS http://*:8087
RUN apt-get -qq update && apt-get -qqy --no-install-recommends install wget gnupg \
    git \
    unzip

RUN curl -sL https://deb.nodesource.com/setup_6.x |  bash -
RUN apt-get install -y nodejs
EXPOSE 8087
ENTRYPOINT ["dotnet","TapClientAngular.dll"]
