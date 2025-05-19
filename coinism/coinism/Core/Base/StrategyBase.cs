using coinism.Core.Interfaces;
using coinism.Core.Models;

namespace coinism.Core.Base
{
    /// <summary>
    /// 모든 전략이 상속할 추상 기본 클래스
    /// 공통적으로 Settings 초기화, Result 관리 기능 제공
    /// </summary>
    public abstract class StrategyBase<TInput, TAction, TResult> : IStrategy<TInput, TAction, TResult>
    {
        protected Settings Settings;
        protected TResult Result;

        /// <summary>
        /// 전략 설정 초기화
        /// </summary>
        public virtual void Init(Settings settings)
        {
            Settings = settings;
            Result = CreateDefaultResult();
        }

        /// <summary>
        /// 실행 여부 기본 구현 (모든 입력 허용)
        /// </summary>
        public virtual bool ShouldExecute(TInput input) => true;

        /// <summary>
        /// 전략 실행 진입점 (템플릿 메서드 패턴)
        /// </summary>
        public TAction Execute(TInput input)
        {
            return ExecuteCore(input);
        }

        /// <summary>
        /// 전략 결과 반환
        /// </summary>
        public TResult GetResult() => Result;

        /// <summary>
        /// 전략별 핵심 실행 로직 구현
        /// </summary>
        protected abstract TAction ExecuteCore(TInput input);

        /// <summary>
        /// 초기 결과 객체 생성 (기본값)
        /// </summary>
        protected abstract TResult CreateDefaultResult();
    }
}
