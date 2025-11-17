export interface Employee {
  id?: number;
  name: string;
  position: string;
  phoneNumber: string;
  email: string;
  salary: number;
  hireDate: string;
}

export interface CreateEmployeeDto {
  name: string;
  position: string;
  phoneNumber: string;
  email: string;
  salary: number;
  hireDate: string;
}

export interface UpdateEmployeeDto {
  name?: string;
  position?: string;
  phoneNumber?: string;
  email?: string;
  salary?: number;
  hireDate?: string;
}
