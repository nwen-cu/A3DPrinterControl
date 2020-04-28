def OnGenerateInfill():
	infills = list()
	for shape in shapes:
		infill = list()
		infill.append((shape[0], shape[2]))
		infill.append((shape[1], shape[3]))
		infills.append(infill)
	return infills
