import Container from '@/components/shared/container';
import CalculatorForm from './organisms/calculator-form';
import { FormProvider, useForm } from 'react-hook-form';
import {
  ProbabilityCalculatorFormSchema,
  type ProbabilityCalculatorFormInputs,
} from './calculator.schema';
import { zodResolver } from '@hookform/resolvers/zod';
import CalculatorFormSubmission from './organisms/calculator-form-submission';
import useCalculateFunction from '@/infrastructure/hooks/use-calculate-function';
import { toast } from 'sonner';

const Calculator = () => {
  const { mutateAsync } = useCalculateFunction({
    retry: false,
    onError: error => {
      console.error('Error calculating function:', error);
      toast.error('Could not calculate function. Please try again.');
    },
  });

  const form = useForm<ProbabilityCalculatorFormInputs>({
    resolver: zodResolver(ProbabilityCalculatorFormSchema),
    defaultValues: {
      probabilityA: '',
      probabilityB: '',
    },
    mode: 'onChange',
  });

  const onSubmit = (data: ProbabilityCalculatorFormInputs) => {
    mutateAsync(data);
  };

  return (
    <FormProvider {...form}>
      <form
        id='calculator-form'
        data-testid='calculator-form'
        onSubmit={form.handleSubmit(onSubmit)}
        className='h-full w-[100px] md:w-1/2 items-center justify-center flex flex-col gap-4'
      >
        <Container className='px-5 py-8 m-2 md:m-10 flex flex-col gap-5'>
          <CalculatorForm />
          <CalculatorFormSubmission />
        </Container>
      </form>
    </FormProvider>
  );
};

export default Calculator;
