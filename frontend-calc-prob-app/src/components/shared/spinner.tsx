import { cn } from '@/lib/utils';
import { Loader2 } from 'lucide-react';
import { type VariantProps, cva } from 'class-variance-authority';
import React from 'react';

const spinnerVariants = cva('animate-spin', {
  variants: {
    size: {
      default: 'h-4 w-4',
      sm: 'h-3 w-3',
      lg: 'h-6 w-6',
      xl: 'h-8 w-8',
    },
    variant: {
      default: 'text-muted-foreground',
      primary: 'text-primary',
      destructive: 'text-destructive',
      secondary: 'text-secondary-foreground',
    },
  },
  defaultVariants: {
    size: 'default',
    variant: 'default',
  },
});

interface SpinnerProps
  extends React.HTMLAttributes<HTMLDivElement>,
    VariantProps<typeof spinnerVariants> {
  asChild?: boolean;
}

const Spinner = React.forwardRef<HTMLDivElement, SpinnerProps>(
  ({ className, size, variant, asChild = false, ...props }, ref) => {
    const Comp = asChild ? React.Fragment : 'div';

    return (
      <Comp
        ref={ref}
        className={cn('flex items-center justify-center', className)}
        {...props}
      >
        <Loader2 className={cn(spinnerVariants({ size, variant }))} />
      </Comp>
    );
  }
);

Spinner.displayName = 'Spinner';

// eslint-disable-next-line react-refresh/only-export-components
export { Spinner, spinnerVariants };
