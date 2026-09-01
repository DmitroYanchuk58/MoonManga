import "./badge.css";

interface BadgeProps {
  label: string;
}

export const Badge = ({ label }: BadgeProps) => (
  <span className="ui-badge">{label}</span>
);
