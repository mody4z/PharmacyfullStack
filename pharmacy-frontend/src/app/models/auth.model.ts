export interface LoginDto {
  username: string;
  password: string;
}

export interface RegisterDto {
  username: string;
  email: string;
  password: string;
  employeeName: string;
  position: string;
  phoneNumber: string;
  salary: number;
}

export interface AuthResponse {
  token: string;
  username: string;
  email: string;
  employeeId?: number;
  employeeName?: string;
  expiration: string;
}
