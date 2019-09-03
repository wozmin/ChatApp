FROM microsoft/dotnet:2.2-sdk as build

ARG BUILDCONFIG=RELEASE
ARG VERSION=1.0.0

COPY . /build/

RUN dotnet restore ./build

COPY . ./build/
WORKDIR /build/
RUN dotnet publish ./ChatServer/ChatServer.csproj -c $BUILDCONFIG -o out 

FROM microsoft/dotnet:2.2-aspnetcore-runtime
WORKDIR /app

COPY --from=build /build/ChatServer/out .

ENTRYPOINT ["dotnet", "ChatServer.dll", "--server.urls", "http://0.0.0.0:5000"] 