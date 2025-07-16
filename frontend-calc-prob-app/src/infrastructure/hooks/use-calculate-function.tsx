import type { CalculationRequest } from '@/core/requests/calculation-request';
import type { CalculationResponse } from '@/core/responses/calculation-response';
import type { ProbabilityCalculatorFormInputs } from '@/features/calculator/calculator.schema';
import { useMutation, type UseMutationOptions } from '@tanstack/react-query';

type useCalculateFunctionProps = Omit<
  UseMutationOptions<
    CalculationResponse,
    Error,
    ProbabilityCalculatorFormInputs
  >,
  'mutationFn'
>;

const useCalculateFunction = (options: useCalculateFunctionProps) => {
  const apiUrl = import.meta.env.VITE_API_URL || 'http://localhost:5014';

  return useMutation<
    CalculationResponse,
    Error,
    ProbabilityCalculatorFormInputs
  >({
    mutationKey: ['calculationResult'],
    mutationFn: async (data: ProbabilityCalculatorFormInputs) => {
      // TODO Ideally this URL should be an environment variable
      // or a configuration setting.
      // For now, it is hardcoded to the local backend URL.
      const request: CalculationRequest = {
        a: data.probabilityA,
        b: data.probabilityB,
      };

      const response = await fetch(`${apiUrl}/api/calculator/${data.action}`, {
        method: 'POST',
        body: JSON.stringify(request),
        headers: { 'Content-Type': 'application/json' },
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`HTTP ${response.status}: ${errorText}`);
      }

      const body: CalculationResponse = await response.json();

      return body;
    },
    ...options,
  });
};

export default useCalculateFunction;
