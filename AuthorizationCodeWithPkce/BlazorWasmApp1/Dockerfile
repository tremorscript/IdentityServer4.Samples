FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BlazorWasmApp1/BlazorWasmApp1.csproj", "BlazorWasmApp1/"
RUN dotnet restore "BlazorWasmApp1/BlazorWasmApp1.csproj"
COPY . .
WORKDIR "/src/BlazorWasmApp1"
RUN dotnet build "BlazorWasmApp1.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "BlazorWasmApp1.csproj" -c Release -o /app/publish 

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf
