using Microsoft.Extensions.DependencyInjection;
using PeerLib.Data;
using PeerLib.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class BlockChainConfig
    {


        public static IServiceCollection AddBlockChainServices(
        this IServiceCollection services)
        {
            services.AddSingleton<AppModel>();
            services.AddScoped<MsgService>();
            services.AddScoped<MsgIndexService>();
            services.AddScoped<BlockService>();
            services.AddScoped<BlockIndexService>();

            return services;
        }
    }
}
