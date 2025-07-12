import Container from '@/components/shared/container';
import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';

const Calculator = () => {
  return (
    <Container className='w-full md:w-1/2 h-full px-5 py-8 m-2 md:m-10 flex flex-col gap-10'>
      <div className='flex w-full justify-center gap-4'>
        <Input variant='brutalist' />
        <Input variant='brutalist' />
      </div>
      <h5 className='text-xl font-bold italic'>
        Choose your calculation function:
      </h5>
      <div className='flex w-full justify-center gap-4'>
        <Button variant='brutalist'>P(A)P(B)</Button>
        <Button variant='brutalist'>P(A) + P(B) – P(A)P(B)</Button>
      </div>
    </Container>
  );
};

export default Calculator;
