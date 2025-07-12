import { cn } from '@/lib/utils';
import { type PropsWithChildren } from 'react';

type SubHeadingProps = {
  className?: string;
};

const SubHeading = ({
  className,
  children,
}: PropsWithChildren<SubHeadingProps>) => {
  return (
    <h2 className={cn('text-xl font-bold italic', className)}>{children}</h2>
  );
};

export default SubHeading;
