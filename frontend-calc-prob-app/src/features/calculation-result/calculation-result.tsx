import type { CalculationResponse } from '@/core/responses/calculation-response';
import { useMutationState } from '@tanstack/react-query';
import Result from './organisms/result';

const CalculationResult = () => {
  const calculationResults = useMutationState({
    filters: { mutationKey: ['calculationResult'] },
    select: mutation => ({
      data: mutation.state.data as CalculationResponse | undefined,
      isLoading: mutation.state.status === 'pending',
      isError: mutation.state.status === 'error',
    }),
  });

  return calculationResults.length === 0 && calculationResults ? null : (
    <Result
      isLoading={calculationResults.at(-1)?.isLoading || false}
      result={calculationResults.at(-1)?.data?.result || 0}
    />
  );
};

export default CalculationResult;
