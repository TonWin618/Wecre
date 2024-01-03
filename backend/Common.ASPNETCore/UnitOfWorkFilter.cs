using Common.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Transactions;

namespace Common.ASPNETCore;
public class UnitOfWorkFilter : IAsyncActionFilter
{
    private readonly IMediator mediator;
    public UnitOfWorkFilter(IMediator mediator)
    {
        this.mediator = mediator;
    }
    private static UnitOfWorkAttribute? GetUnitOfWorkAttribute(ActionDescriptor actionDesc)
    {
        var caDesc = actionDesc as ControllerActionDescriptor;
        if (caDesc == null)
        {
            return null;
        }
        //try to get UnitOfWorkAttribute from controller,
        //if there is no UnitOfWorkAttribute on controller, 
        //try to get UnitOfWorkAttribute from action
        var uowAttr = caDesc.ControllerTypeInfo
            .GetCustomAttribute<UnitOfWorkAttribute>();
        if (uowAttr != null)
        {
            return uowAttr;
        }
        else
        {
            return caDesc.MethodInfo
                .GetCustomAttribute<UnitOfWorkAttribute>();
        }
    }
    public async Task OnActionExecutionAsync(ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var uowAttr = GetUnitOfWorkAttribute(context.ActionDescriptor);
        if (uowAttr == null)
        {
            await next();
            return;
        }
        using TransactionScope txScope = new(TransactionScopeAsyncFlowOption.Enabled);
        List<DbContext> dbCtxs = new List<DbContext>();
        foreach (var dbCtxType in uowAttr.DbContextTypes)
        {
            var sp = context.HttpContext.RequestServices;
            DbContext dbCtx = (DbContext)sp.GetRequiredService(dbCtxType);
            dbCtxs.Add(dbCtx);
        }
        var result = await next();
        if (result.Exception == null)
        {
            foreach (var dbCtx in dbCtxs)
            {
                await dbCtx.SaveChangesAsync();
                if(mediator!= null)
                {
                    await mediator.DispatchDomainEventsAsync(dbCtx);
                }
            }
            txScope.Complete();
        }
    }
}