using System;
using coinism.Core.Models;

namespace coinism.Core.Interfaces
{
    /// <summary>
    /// 자동매매 전략 인터페이스 정의
    /// </summary>
    /// <typeparam name="TInput">입력 데이터 타입 (예: Candle)</typeparam>
    /// <typeparam name="TAction">실행 결과 타입 (예: TradeAction)</typeparam>
    /// <typeparam name="TResult">전략 수행 결과 리포트 타입</typeparam>
    public interface IStrategy<TInput, TAction, TResult>
    {
        /// <summary>
        /// 전략 초기화
        /// </summary>
        void Init(Settings settings);

        /// <summary>
        /// 입력에 따라 매매 조건을 만족하는지 여부 판단
        /// </summary>
        bool ShouldExecute(TInput input);

        /// <summary>
        /// 조건이 만족되었을 때 실행할 액션 반환
        /// </summary>
        TAction Execute(TInput input);

        /// <summary>
        /// 전략 수행 결과 또는 통계 반환
        /// </summary>
        TResult GetResult();
    }
}
