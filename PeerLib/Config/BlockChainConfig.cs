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
            services.AddSingleton<BlovkChainAppModel>();
            services.AddSingleton<BlockChainService>();

            
            services.AddScoped<MsgService>();
            services.AddScoped<MsgQueryService>();

            services.AddScoped<MsgIndexService>();
            services.AddScoped<BlockService>();
            services.AddScoped<BlockIndexService>();
            services.AddScoped<NodeServices>();
            services.AddScoped<MsgSign>();
            services.AddScoped<MsgHashService>();
            services.AddScoped<BalanceService>();


            return services;
        }
        public static IServiceCollection AddWalletServices(
 this IServiceCollection services)
        {
            services.AddSingleton<BlovkChainAppModel>();
            services.AddSingleton<WalletAppModel>();
            services.AddScoped<MsgService>();
            services.AddScoped<MsgIndexService>();
            services.AddScoped<BlockService>();
            services.AddScoped<BlockIndexService>();
            services.AddScoped<NodeServices>();
            services.AddScoped<MsgSign>();
            services.AddScoped<MsgHashService>();
            services.AddScoped<KeyGeneratorService>();

            
            return services;
        }
    }
}
