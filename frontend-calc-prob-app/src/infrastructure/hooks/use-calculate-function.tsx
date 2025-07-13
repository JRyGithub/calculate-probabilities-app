import type { ProbabilityCalculatorFormInputs } from '@/features/calculator/calculator.schema';
import { useMutation, type UseMutationOptions } from '@tanstack/react-query';

type useCalculateFunctionProps = Omit<
  UseMutationOptions<
    ProbabilityCalculatorFormInputs,
    Error,
    ProbabilityCalculatorFormInputs
  >,
  'mutationFn'
>;
const useCalculateFunction = (options: useCalculateFunctionProps) => {
  return useMutation<
    ProbabilityCalculatorFormInputs,
    Error,
    ProbabilityCalculatorFormInputs
  >({
    mutationKey: ['calculationResult'],
    mutationFn: async (data: ProbabilityCalculatorFormInputs) => {
      console.log('Calculating function with data:', data);
      return new Promise(resolve => {
        setTimeout(() => {
          resolve(data);
          return 43;
        }, 1000);
      });
    },
    onError: error => {
      return console.error('Error calculating function:', error);
    },
    ...options,
  });
};

export default useCalculateFunction;
