import RHFInput from '@/components/shared/rhf-form';

const CalculatorForm = () => {
  return (
    <div className='flex w-full justify-center gap-4'>
      <RHFInput
        name='probabilityA'
        type='number'
        label='Probability A'
        className='no-spinner'
      />
      <RHFInput
        name='probabilityB'
        type='number'
        label='Probability B'
        className='no-spinner'
      />
    </div>
  );
};

export default CalculatorForm;
