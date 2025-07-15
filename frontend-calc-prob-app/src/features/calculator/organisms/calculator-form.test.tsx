import { render, screen } from '@testing-library/react';
import { describe, expect, test, vi } from 'vitest';
import { FormProvider, useForm } from 'react-hook-form';
import CalculatorForm from './calculator-form';
import type { ProbabilityCalculatorFormInputs } from '../calculator.schema';

// Mock the RHFInput component
vi.mock('@/components/shared/rhf-form', () => ({
  default: ({
    name,
    label,
    type,
    className,
  }: {
    name: string;
    label: string;
    type: string;
    className?: string;
  }) => (
    <div data-testid={`input-${name}`}>
      <label>{label}</label>
      <input name={name} type={type} className={className} />
    </div>
  ),
}));

const renderWithFormProvider = (component: React.ReactElement) => {
  const TestWrapper = () => {
    const methods = useForm<ProbabilityCalculatorFormInputs>({
      defaultValues: {
        probabilityA: '',
        probabilityB: '',
      },
    });

    return <FormProvider {...methods}>{component}</FormProvider>;
  };

  return render(<TestWrapper />);
};

describe('CalculatorForm', () => {
  test('renders both probability input fields', () => {
    renderWithFormProvider(<CalculatorForm />);

    expect(screen.getByTestId('input-probabilityA')).toBeInTheDocument();
    expect(screen.getByTestId('input-probabilityB')).toBeInTheDocument();
  });

  test('displays correct labels for input fields', () => {
    renderWithFormProvider(<CalculatorForm />);

    expect(screen.getByText('Probability A')).toBeInTheDocument();
    expect(screen.getByText('Probability B')).toBeInTheDocument();
  });

  test('input fields have correct types and classes', () => {
    renderWithFormProvider(<CalculatorForm />);

    // Check that the input containers are present
    const inputA = screen.getByTestId('input-probabilityA');
    const inputB = screen.getByTestId('input-probabilityB');

    expect(inputA).toBeInTheDocument();
    expect(inputB).toBeInTheDocument();

    // Check for the actual input elements within the containers
    const actualInputA = inputA.querySelector('input');
    const actualInputB = inputB.querySelector('input');

    expect(actualInputA).toHaveAttribute('type', 'number');
    expect(actualInputB).toHaveAttribute('type', 'number');
    expect(actualInputA).toHaveClass('no-spinner');
    expect(actualInputB).toHaveClass('no-spinner');
  });

  test('has responsive layout classes', () => {
    const { container } = renderWithFormProvider(<CalculatorForm />);

    const wrapper = container.firstChild as HTMLElement;
    expect(wrapper).toHaveClass(
      'flex',
      'flex-col',
      'md:flex-row',
      'w-full',
      'items-center',
      'justify-center',
      'gap-4'
    );
  });
});
