import { render, screen } from '@testing-library/react';
import { describe, expect, test, vi } from 'vitest';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import Calculator from './calculator';

// Mock the toast function
vi.mock('sonner', () => ({
  toast: {
    error: vi.fn(),
  },
}));

// Mock the hook
vi.mock('@/infrastructure/hooks/use-calculate-function', () => ({
  default: vi.fn(() => ({
    mutateAsync: vi.fn(),
  })),
}));

// Mock the child components
vi.mock('../organisms/calculator-form', () => ({
  default: () => <div data-testid='calculator-form'>Calculator Form</div>,
}));

vi.mock('../organisms/calculator-form-submission', () => ({
  default: () => (
    <div data-testid='calculator-form-submission'>
      Calculator Form Submission
    </div>
  ),
}));

vi.mock('@/components/shared/container', () => ({
  default: ({
    children,
    className,
  }: {
    children: React.ReactNode;
    className?: string;
  }) => (
    <div data-testid='container' className={className}>
      {children}
    </div>
  ),
}));

const renderWithQueryClient = (component: React.ReactElement) => {
  const queryClient = new QueryClient({
    defaultOptions: {
      queries: { retry: false },
      mutations: { retry: false },
    },
  });

  return render(
    <QueryClientProvider client={queryClient}>{component}</QueryClientProvider>
  );
};

describe('Calculator', () => {
  test('renders the calculator form', () => {
    renderWithQueryClient(<Calculator />);

    expect(screen.getByTestId('calculator-form')).toBeInTheDocument();
  });

  test('renders the calculator form submission', () => {
    renderWithQueryClient(<Calculator />);

    expect(
      screen.getByTestId('calculator-form-submission')
    ).toBeInTheDocument();
  });

  test('renders within a container component', () => {
    renderWithQueryClient(<Calculator />);

    expect(screen.getByTestId('container')).toBeInTheDocument();
  });

  test('form has correct id attribute', () => {
    renderWithQueryClient(<Calculator />);

    // Look for the form element by ID since getByRole might not work with mocked components
    const form = document.getElementById('calculator-form');
    expect(form).toBeInTheDocument();
    expect(form).toHaveAttribute('id', 'calculator-form');
  });

  test('form has responsive layout classes', () => {
    renderWithQueryClient(<Calculator />);

    const form = document.getElementById('calculator-form');
    expect(form).toHaveClass(
      'h-full',
      'w-[100px]',
      'md:w-1/2',
      'items-center',
      'justify-center',
      'flex',
      'flex-col',
      'gap-4'
    );
  });

  test('container has correct styling classes', () => {
    renderWithQueryClient(<Calculator />);

    const container = screen.getByTestId('container');
    expect(container).toHaveClass(
      'px-5',
      'py-8',
      'm-2',
      'md:m-10',
      'flex',
      'flex-col',
      'gap-5'
    );
  });

  test('form is configured with react-hook-form', () => {
    renderWithQueryClient(<Calculator />);

    // The form should be present and have the FormProvider context
    const form = document.getElementById('calculator-form');
    expect(form).toBeInTheDocument();

    // The child components should render, indicating FormProvider is working
    expect(screen.getByTestId('calculator-form')).toBeInTheDocument();
    expect(
      screen.getByTestId('calculator-form-submission')
    ).toBeInTheDocument();
  });

  test('renders all child components together', () => {
    renderWithQueryClient(<Calculator />);

    // All main components should be present
    const form = document.getElementById('calculator-form');
    expect(form).toBeInTheDocument();
    expect(screen.getByTestId('container')).toBeInTheDocument();
    expect(screen.getByTestId('calculator-form')).toBeInTheDocument();
    expect(
      screen.getByTestId('calculator-form-submission')
    ).toBeInTheDocument();
  });
});
