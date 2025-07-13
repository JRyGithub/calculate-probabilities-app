import { QueryClient } from '@tanstack/react-query';

const CalculationResult = () => {
  const queryClient = new QueryClient();
  const mutationState = queryClient
    .getMutationCache()
    .find({ mutationKey: ['calculationResult'] });

  if (!mutationState?.state.data) return null;

  return <div>CalculationResult</div>;
};

export default CalculationResult;
