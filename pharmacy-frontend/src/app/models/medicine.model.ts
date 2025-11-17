export interface Medicine {
  id?: number;
  name: string;
  category: string;
  manufacturer: string;
  description: string;
  price: number;
  stockQuantity: number;
  expiryDate: string;
}

export interface CreateMedicineDto {
  name: string;
  category: string;
  manufacturer: string;
  description: string;
  price: number;
  stockQuantity: number;
  expiryDate: string;
}

export interface UpdateMedicineDto {
  name?: string;
  category?: string;
  manufacturer?: string;
  description?: string;
  price?: number;
  stockQuantity?: number;
  expiryDate?: string;
}
