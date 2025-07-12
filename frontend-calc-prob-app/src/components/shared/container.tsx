import { cn } from '@/lib/utils';
import { type PropsWithChildren } from 'react';

type ContainerProps = {
  className?: string;
};

const Container = ({
  className,
  children,
}: PropsWithChildren<ContainerProps>) => {
  return (
    <div className={cn('brutalist-border-large bg-yellow-300', className)}>
      {children}
    </div>
  );
};

export default Container;
