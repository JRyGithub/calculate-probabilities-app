import { Button } from '@/components/ui/button';
import { useFormContext } from 'react-hook-form';
import type { ProbabilityCalculatorFormInputs } from '../calculator.schema';
import { CalculatorFormSubmissionTypes } from '../calculator.props';

const CalculatorFormSubmission = () => {
  const { register, setValue, trigger } =
    useFormContext<ProbabilityCalculatorFormInputs>();

  const handleActionSelect = async (action: string) => {
    setValue('action', action);

    const isValid = await trigger();
    if (isValid) {
      document
        .getElementById('calculator-form')
        ?.dispatchEvent(
          new Event('submit', { bubbles: true, cancelable: true })
        );
    }
  };

  return (
    <div className='flex flex-col gap-4 items-center'>
      <h5 className='text-xl font-bold italic'>
        Choose your calculation function:
      </h5>
      <div className='flex w-full justify-center gap-4'>
        {CalculatorFormSubmissionTypes.map(func => {
          return (
            <span key={func.value}>
              <input type='hidden' {...register('action')} />
              <Button
                variant='brutalist'
                type='button'
                onClick={() => handleActionSelect(func.value)}
              >
                {func.label}
              </Button>
            </span>
          );
        })}
      </div>
    </div>
  );
};

export default CalculatorFormSubmission;
