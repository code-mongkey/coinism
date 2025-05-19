using System;
using System.Threading;
using System.Threading.Tasks;

namespace coinism.Core.Interfaces
{
    /// <summary>
    /// 백테스트 및 실전매매 엔진 공통 인터페이스
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 엔진 실행 진입점
        /// </summary>
        Task RunAsync(CancellationToken token);
    }
}
