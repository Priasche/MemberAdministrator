using Microsoft.AspNetCore.Mvc;
using PM.Buisness.Repositories.IRepositories;
using PM.Data.Entity;
using PM.Models;
using System.Net;

namespace PM.API.Endpoints
{
    public static class MemberEndpoints
    {
        public static void ConfigureMemberEndpoints(this WebApplication app)
        {
            app.MapGet("/api/member", GetAllMember)
                            .WithName("GetMembers")
                .Produces<APIResponse>(200);


            app.MapGet("/api/member/{id:int}", GetMember)
                .WithName("GetMember")
                .Produces<APIResponse>(200);

            app.MapPost("/api/member", CreateMember)
                .WithName("CreateMember")
                .Produces<APIResponse>(201)
                .Produces(400);

            app.MapPut("/api/member", UpdateMember)
                .WithName("UpdateMember")
                .Produces<APIResponse>(200)
                .Produces(400);

            app.MapDelete("/api/member/{id:int}", DeleteMember);
        }

        private async static Task<IResult> DeleteMember(IMemberRepository MemberRepository, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            Member MemberFromStore = await MemberRepository.GetAsync(id);
            if (MemberFromStore != null)
            {
                await MemberRepository.RemoveAsync(MemberFromStore.Id);
                //await MemberRepository.SaveAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("Invalid Id");
                return Results.BadRequest(response);
            }
        }

        private async static Task<IResult> UpdateMember(
            IMemberRepository MemberRepository,
            [FromBody] Member member)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            await MemberRepository.UpdateAsync(member);
            //await MemberRepository.SaveAsync();

            response.Result = await MemberRepository.GetAsync(member.Id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }


        private async static Task<IResult> CreateMember(
            IMemberRepository MemberRepository,
            [FromBody] Member member)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            if (MemberRepository.GetAsync(member.FirstName).GetAwaiter().GetResult() != null)
            {
                response.ErrorMessages.Add("Member name already exists");
                return Results.BadRequest(response);
            }

            Member Member = member;

            await MemberRepository.CreateAsync(Member);
            //await MemberRepository.SaveAsync();

            Member couponDTO = Member;
            response.Result = couponDTO;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);


        }


        public static async Task<IResult> GetAllMember(IMemberRepository _MemberRepo, ILogger<Program> _logger)
        {
            APIResponse response = new();
            _logger.Log(LogLevel.Information, "Getting all Members");
            var members = await _MemberRepo.GetAllAsync();
            response.Result = members.ToList();
            if (members.Count() == 0)
            {
                response.Result = new List<Member>() {
                new Member()
                {
                    FirstName = "asf",
                    LastName = "asfaf",
                    Email = "hsfaf",
                    PhoneNumber = "1234567890"
                }
                };
            }
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response.Result);
        }

        private async static Task<IResult> GetMember(
            IMemberRepository _MemberRepo, ILogger<Program> _logger, int id)
        {
            APIResponse response = new();
            response.Result = await _MemberRepo.GetAsync(id);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

    }
}
