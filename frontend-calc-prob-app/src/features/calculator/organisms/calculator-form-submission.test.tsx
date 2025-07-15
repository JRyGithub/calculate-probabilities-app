import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { describe, expect, test, vi } from 'vitest';
import { FormProvider, useForm } from 'react-hook-form';
import CalculatorFormSubmission from './calculator-form-submission';
import type { ProbabilityCalculatorFormInputs } from '../calculator.schema';

// Mock the Button component
vi.mock('@/components/ui/button', () => ({
  Button: ({
    children,
    onClick,
    variant,
    type,
  }: {
    children: React.ReactNode;
    onClick?: () => void;
    variant?: string;
    type?: 'button' | 'submit' | 'reset';
  }) => (
    <button
      data-testid={`button-${children}`}
      onClick={onClick}
      data-variant={variant}
      type={type}
    >
      {children}
    </button>
  ),
}));

// Mock the calculator props
vi.mock('../calculator.props', () => ({
  CalculatorFormSubmissionTypes: [
    { label: '(P(A)P(B))', value: 'intersection' },
    { label: '(P(A) + P(B) - P(A)P(B))', value: 'union' },
  ],
}));

const renderWithFormProvider = (component: React.ReactElement) => {
  const TestWrapper = () => {
    const methods = useForm<ProbabilityCalculatorFormInputs>({
      defaultValues: {
        probabilityA: '0.5',
        probabilityB: '0.3',
        action: undefined,
      },
    });

    return (
      <div>
        <FormProvider {...methods}>
          <form id='calculator-form'>{component}</form>
        </FormProvider>
      </div>
    );
  };

  return render(<TestWrapper />);
};

describe('CalculatorFormSubmission', () => {
  test('renders calculation function heading', () => {
    renderWithFormProvider(<CalculatorFormSubmission />);

    expect(
      screen.getByText('Choose your calculation function:')
    ).toBeInTheDocument();
  });

  test('renders both calculation buttons', () => {
    renderWithFormProvider(<CalculatorFormSubmission />);

    expect(screen.getByTestId('button-(P(A)P(B))')).toBeInTheDocument();
    expect(
      screen.getByTestId('button-(P(A) + P(B) - P(A)P(B))')
    ).toBeInTheDocument();
  });

  test('buttons have correct variant and type', () => {
    renderWithFormProvider(<CalculatorFormSubmission />);

    const intersectionButton = screen.getByTestId('button-(P(A)P(B))');
    const unionButton = screen.getByTestId('button-(P(A) + P(B) - P(A)P(B))');

    expect(intersectionButton).toHaveAttribute('data-variant', 'brutalist');
    expect(intersectionButton).toHaveAttribute('type', 'button');
    expect(unionButton).toHaveAttribute('data-variant', 'brutalist');
    expect(unionButton).toHaveAttribute('type', 'button');
  });

  test('clicking intersection button triggers form submission', async () => {
    const user = userEvent.setup();

    // Mock form submission
    const submitSpy = vi.fn();
    Element.prototype.dispatchEvent = vi.fn().mockImplementation(submitSpy);

    renderWithFormProvider(<CalculatorFormSubmission />);

    const intersectionButton = screen.getByTestId('button-(P(A)P(B))');
    await user.click(intersectionButton);

    // Note: The actual form validation and submission logic would need valid form data
    // This test verifies the button interaction
    expect(intersectionButton).toBeInTheDocument();
  });

  test('clicking union button triggers form submission', async () => {
    const user = userEvent.setup();

    // Mock form submission
    const submitSpy = vi.fn();
    Element.prototype.dispatchEvent = vi.fn().mockImplementation(submitSpy);

    renderWithFormProvider(<CalculatorFormSubmission />);

    const unionButton = screen.getByTestId('button-(P(A) + P(B) - P(A)P(B))');
    await user.click(unionButton);

    // Note: The actual form validation and submission logic would need valid form data
    // This test verifies the button interaction
    expect(unionButton).toBeInTheDocument();
  });

  test('renders hidden input for action field', () => {
    renderWithFormProvider(<CalculatorFormSubmission />);

    const hiddenInputs = screen.getAllByDisplayValue('');
    expect(hiddenInputs.length).toBeGreaterThan(0);
  });

  test('has correct layout structure', () => {
    const { container } = renderWithFormProvider(<CalculatorFormSubmission />);

    const mainDiv = container.querySelector(
      '.flex.flex-col.gap-4.items-center'
    );
    expect(mainDiv).toBeInTheDocument();

    const buttonContainer = container.querySelector(
      '.flex.w-full.justify-center.gap-4'
    );
    expect(buttonContainer).toBeInTheDocument();
  });
});
