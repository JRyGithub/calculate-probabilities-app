import { render, screen } from '@testing-library/react';
import { describe, expect, test, vi } from 'vitest';
import CalculationResult from './calculation-result';
import { useMutationState } from '@tanstack/react-query';

// Mock the react-query hook
vi.mock('@tanstack/react-query', () => ({
  useMutationState: vi.fn(),
}));

const mockUseMutationState = vi.mocked(useMutationState);

// Mock the Result component
vi.mock('./organisms/result', () => ({
  default: ({ isLoading, result }: { isLoading: boolean; result: number }) => (
    <div data-testid='result-component'>
      <span data-testid='loading'>{isLoading.toString()}</span>
      <span data-testid='result'>{result}</span>
    </div>
  ),
}));

describe('CalculationResult', () => {
  test('renders null when no calculation results', () => {
    mockUseMutationState.mockReturnValue([]);

    const { container } = render(<CalculationResult />);

    expect(container.firstChild).toBeNull();
  });

  test('renders Result component when calculations exist', () => {
    mockUseMutationState.mockReturnValue([
      {
        data: { result: 0.75 },
        isLoading: false,
        isError: false,
      },
    ]);

    render(<CalculationResult />);

    expect(screen.getByTestId('result-component')).toBeInTheDocument();
    expect(screen.getByTestId('loading')).toHaveTextContent('false');
    expect(screen.getByTestId('result')).toHaveTextContent('0.75');
  });

  test('shows loading state when calculation is pending', () => {
    mockUseMutationState.mockReturnValue([
      {
        data: undefined,
        isLoading: true,
        isError: false,
      },
    ]);

    render(<CalculationResult />);

    expect(screen.getByTestId('loading')).toHaveTextContent('true');
    expect(screen.getByTestId('result')).toHaveTextContent('0');
  });

  test('uses latest calculation result when multiple exist', () => {
    mockUseMutationState.mockReturnValue([
      {
        data: { result: 0.25 },
        isLoading: false,
        isError: false,
      },
      {
        data: { result: 0.85 },
        isLoading: false,
        isError: false,
      },
    ]);

    render(<CalculationResult />);

    expect(screen.getByTestId('result')).toHaveTextContent('0.85');
  });

  test('handles missing data gracefully', () => {
    mockUseMutationState.mockReturnValue([
      {
        data: undefined,
        isLoading: false,
        isError: false,
      },
    ]);

    render(<CalculationResult />);

    expect(screen.getByTestId('result')).toHaveTextContent('0');
    expect(screen.getByTestId('loading')).toHaveTextContent('false');
  });
});
