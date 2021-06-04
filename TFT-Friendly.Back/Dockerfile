# --- Build Image ---
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS builder

WORKDIR /app

# --- Copy source ---
COPY . .

# --- Restore ---
RUN dotnet restore

# --- Publish ---
RUN dotnet publish -c Release -o out

# --- Runtime Image ---
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

WORKDIR /app

# --- Copy sources ---
COPY --from=builder /app/out .

# --- Expose Port ---
EXPOSE 5000

ENTRYPOINT ["dotnet", "TFT-Friendly.Back.dll"]