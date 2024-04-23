using Microsoft.EntityFrameworkCore;
using QuackersAPI_DDD.Application.InterfaceService;
using QuackersAPI_DDD.Domain.Model;
using QuackersAPI_DDD.Infrastructure.InterfaceRepository;
using System;

namespace QuackersAPI_DDD.Application.Utilitie.UtilitiesServices
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<string> GenerateRefreshToken(Person person)
        {
            if (person == null)
                throw new ArgumentNullException(nameof(person), "Person cannot be null when generating a refresh token.");

            var refreshToken = new RefreshToken
            {
                Person = person,
                Person_Id = person.Person_Id,
                Token = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                Revoked = false
            };

            await _refreshTokenRepository.AddAsync(refreshToken);
            await _refreshTokenRepository.SaveChangesAsync();
            return refreshToken.Token;
        }

        public async Task<int?> ValidateRefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentException("Refresh token cannot be null or empty.", nameof(refreshToken));

            var token = await _refreshTokenRepository.FindByTokenAsync(refreshToken);
            if (token != null && token.ExpiresAt > DateTime.UtcNow && !token.Revoked)
            {
                return token.Person_Id;
            }
            return null;
        }

        public async Task<bool> RevokeRefreshToken(string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw new ArgumentNullException(nameof(refreshToken), "Refresh token is null or empty.");

            var token = await _refreshTokenRepository.FindByTokenAsync(refreshToken);
            if (token == null)
                throw new KeyNotFoundException("Refresh token not found.");

            if (token.Revoked)
                throw new InvalidOperationException("Token was already revoked.");

            token.Revoked = true;
            await _refreshTokenRepository.UpdateAsync(token);
            await _refreshTokenRepository.SaveChangesAsync();
            return true;
        }

    }
}
