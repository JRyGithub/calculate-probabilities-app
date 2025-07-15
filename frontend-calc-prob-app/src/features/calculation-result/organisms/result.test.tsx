import { render, screen } from '@testing-library/react';
import Result from './result';
import { describe, expect, test } from 'vitest';

describe('Result Component', () => {
  test('renders loading state', () => {
    render(<Result isLoading={true} result={0} />);

    expect(screen.getByText('Calculating...')).toBeInTheDocument();
    expect(screen.queryByText(/Result:/)).not.toBeInTheDocument();
  });

  test('renders result when not loading', () => {
    render(<Result isLoading={false} result={0.7532} />);

    expect(screen.queryByText('Calculating...')).not.toBeInTheDocument();
    expect(screen.getByText('0.75')).toBeInTheDocument();
    expect(screen.getByText('Probability Result')).toBeInTheDocument();
  });

  test('formats result to 2 decimal places', () => {
    render(<Result isLoading={false} result={0.123456} />);

    expect(screen.getByText('0.12')).toBeInTheDocument();
  });

  test('handles zero result', () => {
    render(<Result isLoading={false} result={0} />);

    expect(screen.getByText('0.00')).toBeInTheDocument();
  });

  test('handles result of 1', () => {
    render(<Result isLoading={false} result={1} />);

    expect(screen.getByText('1.00')).toBeInTheDocument();
  });

  test('handles very small decimal results', () => {
    render(<Result isLoading={false} result={0.001} />);

    expect(screen.getByText('0.00')).toBeInTheDocument();
  });

  test('handles negative results (edge case)', () => {
    render(<Result isLoading={false} result={-0.5} />);

    expect(screen.getByText('-0.50')).toBeInTheDocument();
  });

  test('displays result with proper styling when not loading', () => {
    render(<Result isLoading={false} result={0.5} />);

    const resultText = screen.getByText('0.50');
    expect(resultText).toBeInTheDocument();

    // The result should be within the proper container structure
    expect(screen.getByText('Probability Result')).toBeInTheDocument();
  });

  test('shows only loading state when isLoading is true regardless of result value', () => {
    render(<Result isLoading={true} result={0.85} />);

    expect(screen.getByText('Calculating...')).toBeInTheDocument();
    expect(screen.queryByText('0.85')).not.toBeInTheDocument();
    expect(screen.queryByText('Probability Result')).not.toBeInTheDocument();
  });
});
