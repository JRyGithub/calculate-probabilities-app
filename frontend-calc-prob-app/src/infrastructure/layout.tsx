import SubHeading from '@/components/shared/sub-heading';
import Title from '@/components/shared/title';
import CalculationResult from '@/features/calculation-result/calculation-result';
import Calculator from '@/features/calculator/calculator';

/**
 * Layout Page for the Probability Calculator Application
 * Currently as SPA (Single Page Application) with a simple header.
 * Does not require routing or anyything of the sort, so simply renders the calculator component.
 * Later this would render children and wrap all pages.
 */
const Layout = () => {
  return (
    <section className='flex flex-col items-center justify-center gap-4'>
      <Title>Probabilities Calculator</Title>
      <SubHeading>Calculate probabilities with ease!</SubHeading>
      <section className='w-full h-full flex flex-col justify-center items-center'>
        <Calculator />
        <CalculationResult />
      </section>
    </section>
  );
};

export default Layout;
